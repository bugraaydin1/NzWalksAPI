import React, { useState, useEffect } from "react";
import { IQueryParams } from "../../../data/queryParams";
import { IRegion } from "../../../data/region";
import { IWalk } from "../../../data/walk";
import api from "../../../api";
import classes from "./DataTable.module.css";

const DataTable = ({
	name,
	apiEndpoint,
	dataHeaders,
}: {
	name: string;
	apiEndpoint: string;
	dataHeaders: object;
}) => {
	const [data, setData] = useState<IRegion[] | IWalk[]>([]);
	const [page, setPage] = useState(1);
	const [filterOn, setFilterOn] = useState("");
	const [isAscending, setIsAscending] = useState(false);
	const [search, setSearch] = useState("");

	const total = 15;
	const pageSize = 5;
	const sortIcons = ["⬆", "⬇"];

	useEffect(() => {
		const loadData = async () => {
			const params: IQueryParams = {
				page,
				pageSize,
				filterOn,
				filter: search,
				sortBy: filterOn,
				isAscending,
			};

			const response = await api(apiEndpoint, { params });
			setData(response.data);
		};
		apiEndpoint && loadData();
	}, [page, pageSize, search, filterOn, isAscending, apiEndpoint]);

	const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		setSearch(e.target.value);
		setPage(1);
	};

	const handlePageChange = (newPage: number) => {
		setPage(newPage);
	};

	const handleAscending = (e: React.MouseEvent<HTMLElement>) => {
		e.stopPropagation();
		setIsAscending((isAsc) => !isAsc);
	};

	return (
		<div className={classes["container"]}>
			<h2> {name} </h2>
			<div className={classes["search-container"]}>
				<input
					type="text"
					placeholder={
						filterOn
							? `Search by ${filterOn}`
							: "Select a filter from the right"
					}
					value={search}
					onChange={handleSearchChange}
				/>
				<select onChange={(e) => setFilterOn(e.target.value)}>
					<option value=""> Select Field Filter </option>
					{Object.entries(dataHeaders).map(([key, val]) => {
						return (
							key !== "id" &&
							key !== "imageUrl" && (
								<option key={key} value={key}>
									{val}
								</option>
							)
						);
					})}
				</select>
			</div>
			<table className={classes["data-table"]}>
				<thead>
					<tr>
						{Object.entries(dataHeaders).map(([key, header]) => {
							const isSelected = filterOn === key;
							return (
								<th key={key} onClick={(e) => isSelected && handleAscending(e)}>
									{isSelected && sortIcons[isAscending ? 0 : 1]}
									{header}
								</th>
							);
						})}
					</tr>
				</thead>
				<tbody>
					{data.map((item) => (
						<tr key={item.id}>
							{Object.entries(item).map(([key, val]) => {
								const isImage = key === "imageUrl";

								console.log({ isImage, key });

								return (
									<td key={val}>
										{isImage ? <img height={60} src={val} alt={val} /> : val}
									</td>
								);
							})}
						</tr>
					))}
				</tbody>
			</table>
			<div className={classes["pagination"]}>
				{[...Array(Math.ceil(total / pageSize)).keys()].map((_, i) => (
					<button
						key={i}
						onClick={() => handlePageChange(i + 1)}
						className={page === i + 1 ? classes["disabled"] : ""}
						disabled={page === i + 1}
					>
						{i + 1}
					</button>
				))}
			</div>
		</div>
	);
};

export default DataTable;
