import React from 'react';
import { render, screen, fireEvent, waitFor, act } from '@testing-library/react';
import Comments from './Comments';
import * as Api from '../api';

jest.mock('../api');

const fakeComments = [
  { id: 1, userName: 'User1', text: 'Great movie!', createdAt: new Date().toISOString() },
  { id: 2, userName: 'User2', text: 'Amazing!', createdAt: new Date().toISOString() }
];

describe('Comments Component', () => {
  beforeEach(() => {
    jest.spyOn(global, 'fetch').mockResolvedValue({
      ok: true,
      json: async () => fakeComments,
    });
  });

  afterEach(() => {
    jest.restoreAllMocks();
    localStorage.clear();
  });

  test('afișează mesajul de avertizare dacă utilizatorul nu este logat', async () => {
    render(<Comments movieId={123} />);
    expect(screen.getByText(/Trebuie să fii logat pentru a posta comentarii./i)).toBeInTheDocument();
  });

  test('permite postarea unui comentariu atunci când utilizatorul este logat', async () => {

    localStorage.setItem('token', 'dummy-token');
    render(<Comments movieId={123} />);

    await waitFor(() => {
      expect(screen.getByText(/Great movie!/i)).toBeInTheDocument();
    });

    const textarea = screen.getByPlaceholderText(/Scrie un comentariu.../i);
    fireEvent.change(textarea, { target: { value: 'New Comment' } });

    Api.postComment = jest.fn().mockResolvedValue({ message: 'User1' });
  
    const button = screen.getByRole('button', { name: /Postează comentariul/i });
    await act(async () => {
      fireEvent.click(button);
    });

    await waitFor(() => {
      expect(Api.postComment).toHaveBeenCalledWith({ movieId: 123, text: 'New Comment' });
    });
  });
});
