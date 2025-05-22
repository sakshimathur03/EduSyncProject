import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_URL || "https://edusync-backend-api.azurewebsites.net/api";

const api = axios.create({
  baseURL: API_BASE_URL,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
