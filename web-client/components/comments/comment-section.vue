<template>
  <div>
    <comment-input label="comment" @send="(c) => $emit('send',c)" />
    <v-divider v-show="comments" class="my-5" />
    <comment v-for="comment in comments" :key="comment.id" :comment="comment" />
  </div>
</template>

<script>
import CommentInput from './comment-input'
import Comment from './comment'
export default {
  name: 'CommentSection',
  component: { CommentInput, Comment },
  props: {
    comments: {
      require: true,
      type: Array
    }
  }
}
</script>

<style scoped>

</style>
