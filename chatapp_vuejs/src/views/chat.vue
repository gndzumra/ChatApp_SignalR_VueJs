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
      <button large class="btn" @click="send">Gönder</button>
      <button large class="btn second-btn" @click="clear">Clear</button>
    </div>
    <div v-for="(item, list) in listMessage" :key="list">
      <p>{{ item.name }} : {{ item.message }}</p>
    </div>
  </div>
</template>

<script>
/* eslint-disable */
export default {
  created() {
    this.$hub.$on('chat-message-received', this.onMessageReceived)
  },
  beforeDestroy () {
    // Bileşeni kaldırırken SignalR olay işleyicilerini temizlediğinizden emin olun.
    this.$questionHub.$off('chat-message-received', this.onMessageReceived)
  },
  methods: {
    onMessageReceived({ username, text }) {
      this.messages = [...this.messages, { username, text }];
    },
    onSendMessage(evt) {
      this.$questionHub.sendMessage(this.form.message);
      this.form.message = "";
    },
    onHidden() {
      Object.assign(this.form, {
        message: "",
      });
    },
  },
};
</script>
