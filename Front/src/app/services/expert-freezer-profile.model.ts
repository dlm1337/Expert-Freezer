export interface ExpertFreezerProfile {
  id: number;
  companyName: string;
  profilePic: string | null;
  extraPics: string[] | null;
  extraPicsDesc: string[] | null;
  companyDescription: string | null;
  services: string | null;
  address: string | null;
  pricing: string | null;
}