import Vue from 'vue'
import App from './App.vue'
import * as VueSignalR from '@aspnet/signalr'

Vue.use(VueSignalR, 'http://localhost:8080/chat')

process.env.NODE_ENV === 'development' ? "https://localhost:5001/chat" :  "";

new Vue({
  render: h => h(App),
}).$mount('#app')