import { FormEventHandler, useState } from "react";
import { registerAPI } from "../../api/endpoints";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import api from "../../api";
import classes from "./Signup.module.css";

export default function Signup() {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState("");

	const navigate = useNavigate();

	const handleSubmit: FormEventHandler = async (e) => {
		e.preventDefault();

		try {
			const response = await api.post(registerAPI(), {
				username: email,
				password,
			});

			if (response.status === 201) {
				setError("");
				navigate("/app/login");
			}
		} catch (error) {
			if (axios.isAxiosError(error) && error.response) {
				setError(error.response.data);
			}
		}
	};

	return (
		<div className={classes["container"]}>
			<h2>Signup</h2>
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
				<button type="submit">Signup</button>
				{error && <div className={classes["error-message"]}> {error} </div>}
				<Link to="/app/login"> Already have account?</Link>
			</form>
		</div>
	);
}
