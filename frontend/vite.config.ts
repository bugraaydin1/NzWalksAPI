import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => ({
	base: "/app",
	server: {
		proxy: {
			"/api": {
				target:
					mode === "development"
						? "https://localhost:7148"
						: "https://app-nzwalks-eastus-dev-002.azurewebsites.net/",
				changeOrigin: true,
			},
		},
		https: true,
		port: 3001,
	},
	plugins: [react(), mkcert()],
}));
