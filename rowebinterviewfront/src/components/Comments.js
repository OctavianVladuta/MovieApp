import React, { useEffect, useState } from "react";
import { postComment } from "../api";

function Comments({ movieId }) {
  const [commentText, setCommentText] = useState("");
  const [comments, setComments] = useState([]);
  const token = localStorage.getItem("token");

  useEffect(() => {
    async function fetchComments() {
      try {
        const response = await fetch(`https://localhost:7022/api/comments/${movieId}`);
        if (!response.ok) {
          throw new Error("Eroare la preluarea comentariilor");
        }
        const data = await response.json();
        setComments(data);
      } catch (err) {
        console.error(err);
      }
    }
    fetchComments();
  }, [movieId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!token) {
      alert("Trebuie să fii logat pentru a posta un comentariu.");
      return;
    }
    try {
      await postComment({ movieId, text: commentText});
      setCommentText("");
      // reincarcam lista de comentarii
      const response = await fetch(`https://localhost:7022/api/comments/${movieId}`);
      const data = await response.json();
      setComments(data);
    } catch (error) {
      console.error("Eroare la postarea comentariului:", error);
    }
  };

  return (
    <div className="mt-4">
      <h4>Comentarii</h4>
      {comments.length === 0 ? (
        <p>Nu există comentarii încă.</p>
      ) : (
        <ul className="list-group mb-3">
          {comments.map((c) => (
            <li key={c.id} className="list-group-item">
              <strong>{c.userName}:</strong> {c.text}
              <div className="text-muted" style={{ fontSize: "0.8rem" }}>
                {new Date(c.createdAt).toLocaleString()}
              </div>
            </li>
          ))}
        </ul>
      )}

      {token ? (
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <textarea
              className="form-control"
              value={commentText}
              onChange={(e) => setCommentText(e.target.value)}
              placeholder="Scrie un comentariu..."
              required
            />
          </div>
          <button type="submit" className="btn btn-primary">
            Postează comentariul
          </button>
        </form>
      ) : (
        <p className="text-danger">Trebuie să fii logat pentru a posta comentarii.</p>
      )}
    </div>
  );
}

export default Comments;
