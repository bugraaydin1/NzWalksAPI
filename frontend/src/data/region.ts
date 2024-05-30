export interface IRegion {
	id: string;
	code?: string;
	name: string;
	imageUrl?: string;
}

export const RegionHeaders = {
	id: "ID",
	code: "Code",
	name: "Name",
	imageUrl: "Image",
};
