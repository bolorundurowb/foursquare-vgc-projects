import axios from 'axios';
import Cookies from 'js-cookie';

class Api {
  constructor() {
    const service = axios.create({
      baseURL: process.env.VUE_APP_API_URL || 'http://localhost:5089/'
    });

    const token = Cookies.get('token');

    service.interceptors.request.use((config) => {
      if (token) {
        config.headers.common.Authorization = `Bearer ${token}`;
      }

      return config;
    });

    this.service = service;
  }
}

const api = new Api();

export default api.service;
