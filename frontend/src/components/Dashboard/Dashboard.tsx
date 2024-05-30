import { allRegionsAPI, allWalksV2API } from "../../api/endpoints";
import { RegionHeaders } from "../../data/region";
import { WalkHeaders } from "../../data/walk";
import DataTable from "./DataTable/DataTable";

export default function Dashboard() {
	return (
		<>
			<DataTable
				name="Regions"
				dataHeaders={RegionHeaders}
				apiEndpoint={allRegionsAPI()}
			/>
			<DataTable
				name="Walks"
				dataHeaders={WalkHeaders}
				apiEndpoint={allWalksV2API()}
			/>
		</>
	);
}
