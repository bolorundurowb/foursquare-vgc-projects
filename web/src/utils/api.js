import axios from 'axios';
import Cookies from 'js-cookie';

class Api {
  static token() {
    return Cookies.get('token');
  }

  getToken() {
    return Api.token();
  }

  constructor() {
    const service = axios.create({
      baseURL: process.env.VUE_APP_API_URL || 'http://localhost:5089/'
    });
    
    service.interceptors.request.use((config) => {
      const token = Api.token();

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
