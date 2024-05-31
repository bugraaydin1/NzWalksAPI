import axios from "axios";

const api = axios.create({
	timeout: 5000,
	baseURL:
		import.meta.env.MODE === "development"
			? "https://localhost:7148/api"
			: "https://app-nzwalks-eastus-dev-002.azurewebsites.net/api",
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

api.interceptors.response.use(null, (error) => {
	console.error(error);
});
