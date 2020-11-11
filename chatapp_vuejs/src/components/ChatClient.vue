<template>
  <div class="home">
    <div class="table">
      <div class="row">
        <label>User </label>
        <input v-model="user" placeholder="user" />
      </div>
      <div class="row second">
        <label>Message</label>
        <input v-model="message" placeholder="message" id="messageInput" />
      </div>
      <button large class="btn" @click="send"> Gönder </button>
      <button large class="btn second-btn" @click="clear"> Clear </button>
    </div>
    <div v-for="(item, list) in listMessage" :key="list">
      <p>{{ item.name }} : {{ item.message }}</p>
    </div>
  </div>
</template>

<script>
/* eslint-disable */
//import { Vue } form 'vue-property-decorator'
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
    window.atob('dGhpc2lzdW5zYWZl');
    console.log(window.atob('dGhpc2lzdW5zYWZl'));
    // Connect to our hub
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/chat")
      .configureLogging(signalR.LogLevel.None)
      .build();
    navigator.de = this.connection
    this.connection.start()
      .then(() => {
        console.log("Connection Started");
      })
      .catch((error) => {
        if (!error.response) {
          //this.updateConnectionStatus(ConnectionStatus.Disconnected);
          this.errorStatus = "Error: Network Error";
          console.log("Bağlanamadı");
        } 
        else {
          this.errorStatus = error.response.data.message;
        }
      });
  },

  mounted() {
    navigator.con = this.connection;
    this.connection.on("ReceiveMessage", (user, data) => {
      let insertData = { name: user, message: data };
      this.listMessage.push(insertData);
    });
  },

  methods: {
    send() {
      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection.invoke('SendMessage', this.user, this.message)
        .then(()=>{
          console.log(this.user)
        })
        .catch ((err)=>{
          console.error(err)
        })
        
      } else {
        this.connection.start()
        .then(() =>{
            this.connection.invoke('SendMessage', this.user, this.message)
        })
        .catch ((err)=>{
          console.error(err)
        })
      }
    },
    clear() {
      this.listMessage = [];
    },
  },
};
 
</script>

<style scoped>
.table {
  min-width: 30%;
  max-width: 40%;
  height: 50%;
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
