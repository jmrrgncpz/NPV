import Vue from 'vue'
import App from './App.vue'
import axios from "axios";
import Buefy from 'buefy';
import 'buefy/dist/buefy.min.css'
import '@fortawesome/fontawesome-free/css/all.min.css';
import '@fortawesome/fontawesome-free/js/all.min.js';

Vue.config.productionTip = false
Vue.prototype.$axios = axios;
Vue.use(Buefy);
new Vue({
  render: h => h(App),
}).$mount('#app')
