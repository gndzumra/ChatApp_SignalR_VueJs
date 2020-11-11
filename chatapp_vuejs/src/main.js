import Vue from 'vue'
import App from './App.vue'
import VueSignalR from '@aspnet/signalr'

Vue.use(VueSignalR, 'https://localhost:5001/chat')
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
