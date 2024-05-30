import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";

// https://vitejs.dev/config/
export default defineConfig({
	base: "/app",
	server: {
		proxy: {
			"/api": {
				target: "https://localhost:7148",
				changeOrigin: true,
			},
		},
		https: true,
		port: 3001,
	},
	plugins: [react(), mkcert()],
});
