<template>
  <v-container>
    <v-row>
      <v-col cols="12" md="6" style="text-align: -webkit-left">
        <input v-model="user" placeholder="Name Surname" class="messages" />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="6" style="text-align: -webkit-left">
        <input type="text" v-model="message" placeholder="message" class="messages" />
      </v-col>
    </v-row>
    <div style="margin-top: -80px">
      <button large class="btn" @click="send">Gönder</button>
      <button large class="btn second-btn" @click="clear">Clear</button>
    </div>
    <div v-for="(item, list) in listMessage" :key="list">
      <p>{{ item.name }} : {{ item.message }}</p>
    </div>
  </v-container>
</template>

<script>
/*eslint-disable*/
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
    window.atob("dGhpc2lzdW5zYWZl");
    console.log(window.atob("dGhpc2lzdW5zYWZl"));

    // Connect to our hub
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/chat")
      .configureLogging(signalR.LogLevel.None)
      .build();

    this.connection
      .start()
      .then(() => {})
      .catch((error) => {
        if (!error.response) {
          this.errorStatus = "Error: Network Error";
        } else {
          this.errorStatus = error.response.data.message;
        }
      });
  },

  mounted() {
    this.connection.on("ReceiveMessage", (user, data) => {
      let insertData = { name: user, message: data };
      this.listMessage.push(insertData);
    });
  },

  methods: {
    send() {
      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection
          .invoke("SendMessage", this.user, this.message)
          .then(() => {
            console.log(this.user);
          })
          .catch((err) => {
            console.error(err);
          });
      } else {
        this.connection
          .start()
          .then(() => {
            this.connection.invoke("SendMessage", this.user, this.message);
          })
          .catch((err) => {
            console.error(err);
          });
      }
    },
    clear() {
      this.listMessage = [];
    },
  },
};
</script>

<style scoped>
.btn {
  margin-top: 3%;
  margin-left: -5%;
  cursor: pointer;
  margin-top: 6%;
  font: caption;
}
.second-btn {
  margin-left: 1%;
  font: caption;
}
.messages {
  height: 50px;
  width: -webkit-fill-available;
  border-radius: 20px;
  margin-left: 30%;
}
input:focus,
button:focus {
  outline: none;
}
</style>
