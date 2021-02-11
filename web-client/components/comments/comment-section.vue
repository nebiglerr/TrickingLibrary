<template>
  <div>
    <comment-input label="comment" @send="(c) => $emit('send',c)" />
    <v-divider v-show="comments" class="my-5" />
    <comment v-for="comment in comments" :key="comment.id" :comment="comment" />
  </div>
</template>

<script>
import CommentInput from './comment-input'
export default {
  name: 'CommentSection',
  component: { CommentInput },
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
