<template>
  <div>
    <v-text-field
      v-model="filter"
      outlined
      label="Search"
      placeholder="e.g. cork/kick/flip"
      prepend-inner-icon="mdi-magnify"
    />
    <div v-for="t in filteredTricks" :key="t.id">
      {{ t.id }}-{{ t.name }}-{{ t.description }}
    </div>
  </div>
</template>

<script>
export default {
  props: {
    tricks: {
      required: true,
      type: Array
    }
  },
  data: () => ({
    filter: ''
  }),
  computed: {
    filteredTricks () {
      if (!this.filter) {
        return this.tricks
      }
      const normalize = this.filter.trim().toLowerCase()
      return this.tricks.filter((t) => {
        return t.name.toLowerCase().includes(normalize) || t.description.toLowerCase().includes(normalize)
      })
    }
  }

}
</script>

<style scoped>

</style>
