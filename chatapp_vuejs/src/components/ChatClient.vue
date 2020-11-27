<template>
  <v-container>
    <v-row>
      <v-col cols="5" md="5" style="text-align: -webkit-left">
        <input v-model="form.email" placeholder="Name Surname" class="messages" />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="5" md="5" style="text-align: -webkit-left">
        <input type="text" v-model="form.password" placeholder="message" class="messages" />
      </v-col>
    </v-row>
    <div style="margin-top: 10px">
      <button large class="btn" @click="send">Gönder</button>
      <button large class="btn second-btn" @click="authorize">Authorize</button>
    </div>
    <div v-for="(item, list) in listMessage" :key="list">
      <p>{{ item.name }} : {{ item.message }}</p>
    </div>
  </v-container>
</template>

<script>
/*eslint-disable*/
import * as signalR from "@aspnet/signalr";
import axios from "axios";

export default {
  data() {
    return {
      form: {
        email: "",
        password: "",
      },
      user: "",
      message: "SELAMLAR",
      listMessage: [],
      connection: "",
    };
  },

  created() {
    window.atob("dGhpc2lzdW5zYWZl");
    console.log(window.atob("dGhpc2lzdW5zYWZl"));

    // Connect to our hub
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/live-hub", {
        accessTokenFactory: () => {
          return window.localStorage.getItem("token");
        },
        transport: signalR.HttpTransportType.WebSockets,
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection
      .start()
      .then((aa) => {alert("then")})
      .catch((error) => {
        console.log(error);
      });
  },

  mounted() {
    this.connection.on("LiveChatMessageReceived", (username, data) => {
      let insertData = { username: username, message: data };
      this.listMessage.push(insertData);
    });
  },

  methods: {
    send() {
      if (this.connection.state === signalR.HubConnectionState.Connected) {
        this.connection
          .invoke("SendLiveChatMessage", this.message)
          .then(() => {})
          .catch((err) => {console.log(err)});
      } else {
        this.connection
          .start()
          .then(() => {
            this.connection.invoke("SendLiveChatMessage", this.message);
          })
          .catch((err) => {console.log(err)});
      }
    },

    authorize() {
      return axios.post("http://localhost:5000/account/token", { email: this.form.email, password: this.form.password }).then((res) => {
        localStorage.setItem("token", res.data.token);
      });
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
