import { FormEventHandler, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { loginAPI } from "../../api/endpoints";
import axios from "axios";
import api from "../../api";
import classes from "./Login.module.css";

export default function Login() {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState("");

	const navigate = useNavigate();

	const handleSubmit: FormEventHandler = async (e) => {
		e.preventDefault();

		try {
			const response = await api.post(loginAPI(), {
				username: email,
				password,
			});
			const { jwtToken } = response.data;

			if (response.status === 200 && jwtToken) {
				localStorage.setItem("token", jwtToken);
				setError("");
				navigate("/app/dashboard");
			}
		} catch (error) {
			if (axios.isAxiosError(error) && error.response) {
				setError(error.response.data);
			}
		}
	};

	return (
		<div className={classes.container}>
			<h2>Login</h2>
			<form onSubmit={handleSubmit}>
				<div className={classes["form-group"]}>
					<label>Email</label>
					<input
						type="email"
						value={email}
						onChange={(e) => setEmail(e.target.value)}
						required
					/>
				</div>
				<div className={classes["form-group"]}>
					<label>Password</label>
					<input
						type="password"
						value={password}
						onChange={(e) => setPassword(e.target.value)}
						required
					/>
				</div>
				<button type="submit">Login</button>
				{error && <div className={classes["error-message"]}> {error} </div>}
				<Link to="/app/signup"> No account?</Link>
			</form>
		</div>
	);
}
