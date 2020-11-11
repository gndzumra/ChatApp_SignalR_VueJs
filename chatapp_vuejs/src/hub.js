/* eslint-disable */

import * as signalR from "@aspnet/signalr";

export default {
  install(Vue) {
    const hub = new Vue();
    Vue.prototype.$hub = hub;

    let connection = null;
    let startedPromise = null;
    let manuallyClosed = false;

    Vue.prototype.startSignalR = () => {
      connection = new HubConnectionBuilder()
        .withUrl(`${Vue.prototype.$http.defaults.baseURL}/chat`)
        .configureLogging(LogLevel.Information)
        .build();

      connection.on("ReceiveMessage", (username, text) => {
        questionHub.$emit("chat-message-received", { username, text });
      });
    };
    Vue.prototype.stopSignalR = () => {
      if (!startedPromise) return;

      manuallyClosed = true;
      return startedPromise
        .then(() => connection.stop())
        .then(() => {
          startedPromise = null;
        });
    };

    hub.sendMessage = (message) => {
      if (!startedPromise) return;

      return startedPromise.then(() => connection.invoke("SendMessage", user, message)).catch(console.error);
    };
  },
};
