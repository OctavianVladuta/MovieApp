const API_BASE_URL = "https://localhost:7022/api";

//filme recente
export async function getLatestMovies() {
  const response = await fetch(`${API_BASE_URL}/movies/latest`);
  if (!response.ok) {
    throw new Error("Eroare la preluarea filmelor recente");
  }
  return await response.json();
}

//filme top
export async function getTopMovies() {
  const response = await fetch(`${API_BASE_URL}/movies/top`);
  if (!response.ok) {
    throw new Error("Eroare la preluarea filmelor top");
  }
  return await response.json();
}


//cautare filme dupa titlu sau gen
export async function searchMovies(title, genreId) {
  const url = new URL(`${API_BASE_URL}/movies/search`);
  if (title) url.searchParams.append("title", title);
  if (genreId) url.searchParams.append("genre_ids", genreId);
  
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error("Eroare la căutarea filmelor");
  }
  return await response.json();
}

//detalii film
export async function getMovieDetails(movieId) {
  const response = await fetch(`${API_BASE_URL}/movies/${movieId}`);
  if (!response.ok) {
    throw new Error("Eroare la preluarea detaliilor filmului");
  }
  return await response.json();
}


//inregistrare utilizator
export async function register(email, password, confirmPassword) {
  const response = await fetch(`${API_BASE_URL}/account/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password, confirmPassword })
  });
  if (!response.ok) {
    throw new Error("Eroare la înregistrare");
  }
  return await response.json();
}

//login utilizator
export async function login(email, password) {
  const response = await fetch(`${API_BASE_URL}/account/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password })
  });
  if (!response.ok) {
    throw new Error("Eroare la autentificare");
  }
  return await response.json();
}


//postare comentariu (JWT)
export async function postComment(comment) {
  const token = localStorage.getItem("token");
  const response = await fetch(`${API_BASE_URL}/comments`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(comment)
  });
  if (!response.ok) {
    throw new Error("Eroare la postarea comentariului");
  }
  return await response.json();
}
