import { Outlet } from "react-router-dom";

export default function Layout(props: React.PropsWithChildren) {
	return (
		<>
			<Outlet />
			{props.children}
		</>
	);
}
