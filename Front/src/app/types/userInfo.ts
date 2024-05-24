export class UserInfo {
  id?: number;
  userName: string;
  password: string;
  confirmPassword: string;
  companyName?: string;
  profilePic?: File;
  extraPics?: File[];
  extraPicsDesc?: string[];
  companyDescription: string;
  services: string;
  address?: string;
  pricing: string;
}