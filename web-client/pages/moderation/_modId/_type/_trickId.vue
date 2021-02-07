<template>
  <div>
    <div v-if="item">
      {{ item.description }}
    </div>
    <div v-if="replyId > 0">
      Replying to {{ replyId }}
      <v-btn @click="replyId = 0">
        Clear
      </v-btn>
    </div>
    <div>
      <v-text-field v-model="comment" label="Comments" />
      <v-btn @click="send">
        Send
      </v-btn>
    </div>

    <div v-for="c in comments" :key="c.id" class="my-1">
      <span v-html="c.htmlContent" />
      <v-btn small @click="replyId = c.id">
        Reply
      </v-btn>
      <v-btn small @click="loadReplies(c)">
        Load Reply
      </v-btn>
      <div v-for="r in c.replies" :key="r.id">
        <span v-html="r.htmlContent" />
      </div>
    </div>
  </div>
</template>
<script>
const endpointResolver = (type) => {
  if (type === 'trick') {
    return 'tricks'
  }
}
const commentWithReplies = comment => ({
  ...comment.valueOf(),
  replies: []
})
export default {
  data: () => ({
    item: null,
    comments: [],
    comment: '',
    replyId: 0
  }),

  created () {
    const { modId, type, trickId } = this.$route.params
    const endpoint = endpointResolver(type)
    this.$axios.$get('/api/' + endpoint + '/' + trickId).then((item) => {
      this.item = item
    })
    this.$axios.$get('/api/moderation-items/' + modId + '/comments').then((comments) => {
      this.comments = comments.map(commentWithReplies)
    })
  },
  methods: {
    send () {
      const { modId } = this.$route.params

      if (this.replyId > 0) {
        this.$axios.$post('/api/comments/' + this.replyId + '/replies',
          { content: this.comment }).then((comment) => {
          this.comments.find(x => x.id === this.replyId).replies.push(comment.valueOf())
          this.comment = ''
        })
      } else {
        this.$axios.$post('/api/moderation-items/' + modId + '/comments',
          { content: this.comment }).then((comment) => {
          this.comments.push(commentWithReplies(comment.valueOf()))
          this.comment = ''
        })
      }
    },
    loadReplies (comment) {
      this.$axios.$get('/api/comments/' + comment.id + '/replies').then((comments) => {
        this.$set(comment, 'replies', comments)
      })
    }
  }

}
</script>

<style scoped>

</style>
