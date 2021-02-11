<template>
  <div>
    <v-text-field v-model="content" clearable label="Comments" @keydown.ctrl.enter="$emit('send',content)" />
    <v-btn v-if="$listeners['cancel']" @click="$emit('cancel')">
      Cancel
    </v-btn>
    <v-btn :disabled="!content" @click="$emit('send',content)">
      {{ label }}
    </v-btn>
  </div>
</template>

<script>
export default {
  name: 'CommentInput',
  props: {
    label: {

      require: false,
      type: String,
      default: 'send'
    }
  },
  data: () => ({
    content: ''
  })
}
</script>

<style scoped>

</style>
