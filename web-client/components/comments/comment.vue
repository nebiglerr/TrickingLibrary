<template>
  <div>
    <comment-body :comment="comment" @send="send" @load-replies="loadReplies" />
    <div class="ml-5">
      <comment-body v-for="reply in replies" :key="'reply-'+reply.id" :comment="reply" @send="send" />
    </div>
  </div>
</template>

<script>
import CommentBody from './comment-body'
export default {
  name: 'Comment',
  props: {
    comment: {
      require: true,
      type: Object
    }
  },
  component: { CommentBody },
  data: () => ({
    replies: []
  }),
  methods: {
    send (content) {
      return this.$axios.$post('/api/comments/' + this.comment.id + '/replies',
        { content })
        .then((comment) => {
          this.replies.push(comment.valueOf())
        })
    },
    loadReplies () {
      return this.$axios.$get('/api/comments/' + this.comment.id + '/replies')
        .then((replies) => {
          this.replies = replies.valueOf()
        })
    }
  }
}
</script>

<style scoped>

</style>
