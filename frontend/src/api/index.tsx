import axios from "axios";

const api = axios.create({
	timeout: 5000,
	baseURL: "https://localhost:7148/api",
});

export default api;

api.interceptors.request.use(
	(config) => {
		const token = localStorage.getItem("token");
		if (token) {
			config.headers.Authorization = `Bearer ${token}`;
		}
		return config;
	},
	(error) => Promise.reject(error)
);
