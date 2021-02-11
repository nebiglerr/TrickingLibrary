<template>
  <div>
    <comment-body :comment="comment" @send="send" @load-replies="loadReplies" />
    <div class="ml-5">
      <comment-body v-for="reply in replies" :key="reply.id" :comment="reply" @send="send" />
    </div>
  </div>
</template>

<script>
export default {
  name: 'Comment',
  props: {
    comment: {
      require: true,
      type: Object
    }
  },
  data: () => ({
    replies: []
  }),
  methods: {
    send (content) {
      return this.$axios.$post('/api/comments/' + this.comment.id + '/replies',
        { content }).then((comment) => {
        this.replies.push(comment.valueOf())
      })
    },
    loadReplies () {
      this.$axios.$get('/api/comments/' + this.comment.id + '/replies').then((replies) => {
        this.replies = replies
      })
    }
  }
}
</script>

<style scoped>

</style>
