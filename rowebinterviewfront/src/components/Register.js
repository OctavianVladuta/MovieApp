// src/components/Register.js
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../api";

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      alert("Parolele nu coincid");
      return;
    }
    try {
      await register(email, password, confirmPassword);
      alert("Înregistrare reușită!");
      navigate("/login");
    } catch (err) {
      console.error("Eroare la înregistrare:", err);
      alert("Înregistrare eșuată");
    }
  };

  return (
    <div className="col-md-6 offset-md-3">
      <h2>Register</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Email</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label>Parolă</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="mb-3">
          <label>Confirmare Parolă</label>
          <input
            type="password"
            className="form-control"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
          />
        </div>
        <button className="btn btn-primary" type="submit">
          Înregistrare
        </button>
      </form>
    </div>
  );
}

export default Register;
