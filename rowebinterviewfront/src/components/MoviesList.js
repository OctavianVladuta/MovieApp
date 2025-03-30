import React from "react";
import { Link } from "react-router-dom";

function MoviesList({ movies }) {
  if (!movies || movies.length === 0) {
    return <p>Nu există filme de afișat.</p>;
  }

  return (
    <div className="row">
      {movies.map((movie) => (
        <div className="col-md-3 mb-4" key={movie.id}>
          <div className="card">
            {<img src={`https://image.tmdb.org/t/p/w200${movie.poster_path}`} alt={movie.title} />}
            <div className="card-body">
              <h5 className="card-title">{movie.title}</h5>
              <Link to={`/movies/${movie.id}`} className="btn btn-primary">
                Detalii
              </Link>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default MoviesList;
