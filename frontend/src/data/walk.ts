// import { IRegion } from "./region";

export interface IWalk {
	id: string;
	name: string;
	length: number;
	description?: string;
	imageUrl?: string;
	// region?: IRegion;
	// difficulty?: object
}

export const WalkHeaders = {
	id: "ID",
	name: "Name",
	Length: "Length",
	// description: "Description",
	imageUrl: "Image",
	// region: "Region",
	// difficulty: "Difficulty",
};
