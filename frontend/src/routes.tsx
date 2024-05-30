import { Navigate, createBrowserRouter } from "react-router-dom";
import Login from "./components/Login";
import Layout from "./Layout";
import Signup from "./components/Signup";
import Dashboard from "./components/Dashboard";

export const routes = createBrowserRouter([
	{
		path: "/app",
		element: <Layout />,
		children: [
			{
				index: true,
				element: <Navigate to="login" />,
			},
			{
				path: "login",
				element: <Login />,
			},
			{
				path: "signup",
				element: <Signup />,
			},
			{
				path: "dashboard",
				element: <Dashboard />,
			},
		],
	},
]);
