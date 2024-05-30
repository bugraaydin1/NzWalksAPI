export const registerAPI = () => "/auth/register";
export const loginAPI = () => "/auth/login";

export const imagesAPI = () => "/images";
export const uploadImageAPI = () => "/images/upload";

export const allRegionsAPI = () => "/v1/regions";
export const regionByIdAPI = (id: string) => `/v1/regions/${id}`;

// export const allWalksAPI = () => "/v1/walks";
export const allWalksV2API = () => "/v2/walks";
export const walkByIdAPI = (id: string) => `/v1/walks/${id}`;
