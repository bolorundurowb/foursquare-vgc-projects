import Vue from 'vue';
import format from 'date-fns/format';

import App from './App.vue';
import router from './router';

import ElementUI from 'element-ui';
import './sass/style.scss';

import locale from 'element-ui/lib/locale/lang/en';
Vue.config.productionTip = false;
Vue.use(ElementUI, { locale });

Vue.filter('dateFilter', (value) => {
  return format(new Date(value), 'PPPP')
});

new Vue({
  router,
  render: (h) => h(App)
}).$mount('#app');
