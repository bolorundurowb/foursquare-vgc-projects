import Vue from 'vue';
import App from './App.vue';
import router from './router';
// import VueModal from '@kouts/vue-modal';

import ElementUI from 'element-ui';
import './sass/style.scss';

Vue.config.productionTip = false;

Vue.use(ElementUI);

// Vue.component('VueModal', VueModal);

new Vue({
  router,
  render: (h) => h(App)
}).$mount('#app');
