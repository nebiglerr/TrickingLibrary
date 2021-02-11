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
    <comment-input v-if="replying" @send="(c) => $emit('send',c)" @cancel="replying =false" />
  </div>
</template>

<script>
export default {
  name: 'CommentBody',

  props: {
    comment: {
      require: true,
      type: Object
    }
  },
  data: () => ({
    replying: false
  })
}
</script>

<style scoped>

</style>
