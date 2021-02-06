<template>
  <div>
    <div v-if="item">
      {{ item.description }}
    </div>
    <div>
      <v-text-field v-model="comment" label="Comments" />
      <v-btn @click="send">
        Send
      </v-btn>
    </div>
    <div v-for="c in comments" :key="c.id" v-html="c.htmlContent" />
  </div>
</template>
<script>
const endpointResolver = (type) => {
  if (type === 'trick') {
    return 'tricks'
  }
}
export default {
  data: () => ({
    item: null,
    comments: [],
    comment: ''
  }),

  created () {
    const { modId, type, trickId } = this.$route.params
    const endpoint = endpointResolver(type)
    this.$axios.$get('/api/' + endpoint + '/' + trickId).then((item) => {
      this.item = item
    })
    this.$axios.$get('/api/moderation-items/' + modId + '/comments').then((comments) => {
      this.comments = comments
    })
  },
  methods: {
    send () {
      const { modId } = this.$route.params

      this.$axios.$post('/api/moderation-items/' + modId + '/comments',
        { content: this.comment }).then((comment) => {
        this.comments.push(comment.valueOf())
        this.comment = ''
      })
    }
  }

}
</script>

<style scoped>

</style>
