import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter, Routes, Route } from 'react-router-dom';
import MovieDetails from './MovieDetails';
import * as Api from '../api';

jest.mock('../api');

const fakeMovie = {
  id: 1,
  title: 'Test Movie',
  overview: 'This is a test movie overview.',
  images: {
    backdrops: [
      { file_path: '/backdrop1.jpg' },
      { file_path: '/backdrop2.jpg' },
      { file_path: '/backdrop3.jpg' },
      { file_path: '/backdrop4.jpg' }
    ]
  },
  credits: {
    cast: [
      { id: 101, name: 'Actor One', character: 'Role One', profile_path: '/actorOne.jpg' },
      { id: 102, name: 'Actor Two', character: 'Role Two', profile_path: '/actorTwo.jpg' },
      { id: 103, name: 'Actor Three', character: 'Role Three', profile_path: null }
    ]
  }
};

describe('MovieDetails Component', () => {
  beforeEach(() => {
    Api.getMovieDetails.mockResolvedValue(fakeMovie);
  });

  test('afișează starea de încărcare și apoi detaliile filmului', async () => {
    render(
      <MemoryRouter initialEntries={['/movies/1']}>
        <Routes>
          <Route path="/movies/:id" element={<MovieDetails />} />
        </Routes>
      </MemoryRouter>
    );

    expect(screen.getByText(/Se încarcă detaliile filmului/i)).toBeInTheDocument();

    const titleElement = await screen.findByText('Test Movie');
    expect(titleElement).toBeInTheDocument();

    const overviewElement = await screen.findByText('This is a test movie overview.');
    expect(overviewElement).toBeInTheDocument();

    const images = screen.getAllByRole('img');
    expect(images.length).toBeGreaterThan(0);
  });

});
