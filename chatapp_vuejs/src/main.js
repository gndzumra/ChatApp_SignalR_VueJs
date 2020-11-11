import Vue from 'vue'
import App from './App.vue'
import * as VueSignalR from '@aspnet/signalr'

Vue.use(VueSignalR, 'https://localhost:5001/chat')
Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
