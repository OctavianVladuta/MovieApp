import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMovieDetails } from "../api";
import Comments from "./Comments";

function MovieDetails() {
  const { id } = useParams();
  const [movie, setMovie] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  

  const [showAllImages, setShowAllImages] = useState(false);
  const [showAllActors, setShowAllActors] = useState(false);

  useEffect(() => {
    async function fetchDetails() {
      try {
        const data = await getMovieDetails(id);
        setMovie(data);
      } catch (err) {
        console.error("Eroare la preluarea detaliilor filmului:", err);
      } finally {
        setLoading(false);
      }
    }
    fetchDetails();
  }, [id]);

  if (loading) return <p>Se încarcă detaliile filmului...</p>;
  if (!movie) return <p>Nu s-au găsit detalii pentru acest film.</p>;

  const initialImageCount = 6;
  const initialActorCount = 6;
  const imagesToShow = showAllImages ? movie.images.backdrops : movie.images.backdrops.slice(0, initialImageCount);
  const actorsToShow = showAllActors ? movie.credits.cast : movie.credits.cast.slice(0, initialActorCount);

  return (
    <div className="container my-4">
      {/* Buton de back */}
      <button className="btn btn-secondary mb-3" onClick={() => navigate(-1)}>
        Back
      </button>
      <h2>{movie.title}</h2>
      <p>{movie.overview}</p>

      {/* afisare imagini */}
      {movie.images && movie.images.backdrops && movie.images.backdrops.length > 0 && (
        <div className="mb-4">
          <h3>Imagini</h3>
          <div className="row">
            {imagesToShow.map((backdrop, index) => (
              <div className="col-md-4 mb-3" key={index}>
                <img
                  src={`https://image.tmdb.org/t/p/w500${backdrop.file_path}`}
                  alt="Backdrop"
                  className="img-fluid"
                />
              </div>
            ))}
          </div>
          {movie.images.backdrops.length > initialImageCount && (
            <button
              className="btn btn-link"
              onClick={() => setShowAllImages(!showAllImages)}
            >
              {showAllImages ? "Ascunde" : "Vizualizează mai multe imagini"}
            </button>
          )}
        </div>
      )}

      {/* afisare actori */}
{movie.credits && movie.credits.cast && movie.credits.cast.length > 0 && (
  <div className="mb-4">
    <h3>Actori</h3>
    <ul className="list-group">
      {actorsToShow.map((actor) => (
        <li key={actor.id} className="list-group-item d-flex align-items-center">
          <div style={{ width: 50, height: 75, marginRight: 10 }}>
            {actor.profile_Path ? (
              <img
                src={`https://image.tmdb.org/t/p/w200${actor.profile_Path}`}
                alt={actor.name}
                style={{ width: "50px", height: "75px", objectFit: "cover" }}
              />
            ) : (
              <div
                style={{
                  width: "50px",
                  height: "75px",
                  backgroundColor: "#ccc",
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                  fontSize: "0.8rem",
                }}
              >
                N/A
              </div>
            )}
          </div>
          <div>
            <strong>{actor.name}</strong>
            <span> – rol: {actor.character}</span>
          </div>
        </li>
      ))}
    </ul>

    {movie.credits.cast.length > initialActorCount && (
      <button
        className="btn btn-link mt-2"
        onClick={() => setShowAllActors(!showAllActors)}
      >
        {showAllActors ? "Ascunde" : "Vizualizează mai mulți actori"}
      </button>
    )}
  </div>
)}


      {/* afisare comentarii */}
      <Comments movieId={movie.id} />
    </div>
  );
}

export default MovieDetails;
