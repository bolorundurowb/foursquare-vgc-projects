import Vue from 'vue';
import App from './App.vue';
import router from './router';

import ElementUI from 'element-ui';
import './sass/style.scss';

import locale from 'element-ui/lib/locale/lang/en';
Vue.config.productionTip = false;
Vue.use(ElementUI, { locale });

new Vue({
  router,
  render: (h) => h(App)
}).$mount('#app');
