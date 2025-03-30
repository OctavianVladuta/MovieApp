import React, { useState } from "react";
import { getLatestMovies, getTopMovies, searchMovies } from "../api";
import MoviesList from "./MoviesList";

function Home() {
  const [movies, setMovies] = useState([]);
  const [title, setTitle] = useState("");
  const [genreId, setGenreId] = useState("");

  const handleLatest = async () => {
    try {
      const data = await getLatestMovies();
      setMovies(data);
    } catch (err) {
      console.error(err);
    }
  };

  const handleTop = async () => {
    try {
      const data = await getTopMovies();
      setMovies(data);
    } catch (err) {
      console.error(err);
    }
  };

  const handleSearch = async (e) => {
    e.preventDefault();
    try {
      const genre = genreId ? parseInt(genreId, 10) : undefined;
      const data = await searchMovies(title, genre);
      setMovies(data);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div>
      <h2>Acasă</h2>
      <div className="mb-3">
        <button className="btn btn-primary me-2" onClick={handleLatest}>
          Filme Recente
        </button>
        <button className="btn btn-secondary" onClick={handleTop}>
          Filme Top
        </button>
      </div>

      <form className="row g-3 mb-3" onSubmit={handleSearch}>
        <div className="col-auto">
          <input
            type="text"
            className="form-control"
            placeholder="Titlu film..."
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
        </div>
        <div className="col-auto">
          <input
            type="number"
            className="form-control"
            placeholder="Genre ID (ex. 28)"
            value={genreId}
            onChange={(e) => setGenreId(e.target.value)}
          />
        </div>
        <div className="col-auto">
          <button type="submit" className="btn btn-success">
            Caută
          </button>
        </div>
      </form>

      <MoviesList movies={movies} />
    </div>
  );
}

export default Home;
