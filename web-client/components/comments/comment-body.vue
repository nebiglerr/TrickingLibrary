<template>
  <div>
    <div class="my-1">
      <p class="mb-1" v-html="comment.htmlContent" />
      <v-btn v-if="!replying" small @click="replying =true">
        reply
      </v-btn>
      <v-btn v-if="$listeners['load-replies']" small @click="$emit('load-replies')">
        Load Replies
      </v-btn>
    </div>
    <comment-input v-if="replying" label="Reply" @send="(c) => $emit('send',c)" @cancel="replying =false" />
  </div>
</template>

<script>
import CommentInput from './comment-input'
export default {
  name: 'CommentBody',

  props: {
    comment: {
      require: true,
      type: Object
    }
  },
  component: { CommentInput },
  data: () => ({
    replying: false
  })
}
</script>

<style scoped>

</style>
