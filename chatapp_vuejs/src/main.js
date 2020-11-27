import Vue from 'vue'
import App from './App.vue'
import * as VueSignalR from '@aspnet/signalr'

Vue.use(VueSignalR, 'http://localhost:5000/chat')

new Vue({
  render: h => h(App),
}).$mount('#app')