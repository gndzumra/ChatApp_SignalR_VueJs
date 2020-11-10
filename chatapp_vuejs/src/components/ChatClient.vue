<template>
  <div>
    <div class="table">
      <div class="row">
        <label>User </label>
        <input v-model="user" placeholder="user" />
      </div>
      <div class="row second">
        <label>Message</label>
        <input v-model="message" placeholder="message" id="messageInput" />
      </div>
      <button large class="btn">Gönder</button>
      <button large class="btn second-btn">Clear</button>
    </div>
    <div v-for="(item, list) in listMessage" :key="list">
      <p>{{ item.name }} : {{ item.message }}</p>
    </div>
  </div>
</template>

<script>
/* eslint-disable */
import * as signalR from "@aspnet/signalr";

export default {
  data() {
    return {
      user: "",
      message: "",
      listMessage: [],
      connection: "",
    };
  },

  created() {
    // Connect to our hub
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://127.0.0.1:5001/chat/")
      .configureLogging((logging) => {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Debug);
      }).build();

    this.connection.start()
      .then(() => console.log("Connection Started"))
      .catch((err) => console.log("connecting hub failed err is : ", err));
  },

  //mounted: DOM'a eklendiği anda çalışacak
  mounted() {
    // this.connection.start();

    navigator.con = this.connection;
    this.connection.on("ReceiveMessage", (user, data) => {
      let insertData = { name: user, message: data };
      this.listMessage.push(insertData);
    });
  },

  methods: {
    /*
    send() {
      document.getElementById("messageInput").disabled = true;

      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection.invoke("SendMessage", this.user, this.message);
      } else {
        this.connection.start().then(() => this.connection.invoke("SendMessage", this.user, this.message));
      }
    },
    clear() {
      this.listMessage = [];
    }, 
    */
  },
};
</script>

<style scoped>
.table {
  min-width: 30%;
  max-width: 40%;
  height: 50%;
  border: 2px solid black;
  align-self: center;
  padding: 3%;
}
.row {
  display: flex;
  flex-direction: row;
  justify-content: center;
  justify-items: center;
}
.second {
  margin-top: 4%;
}
.btn {
  margin-top: 5%;
  cursor: pointer;
  margin-top: 10%;
}
.second-btn {
  margin-left: 5%;
}
.row label {
  flex: 1;
}
</style>
