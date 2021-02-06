<template>
  <div>
    <v-btn
      v-for="i in items"
      :key="i.id"
      :to="'/moderation/'+i.type+'/'+i.target"
    >
      {{ i.target }}
    </v-btn>
  </div>
</template>

<script>
export default {
  async fetch () {
    this.items = await this.$axios.$get('/api/moderation-items')
  },
  data: () => ({
    items: []
  })

}
</script>

<style scoped>

</style>
