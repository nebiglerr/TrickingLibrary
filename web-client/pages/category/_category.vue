<template>
  <div class="d-flex mt-3   justify-center align-start">
    <div class="mx-2">
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
    <div class="mx-2 ">
      <v-sheet v-if="category" class="pa-3 ma-2 sticky">
        <div class="text-h6">
          {{ category.name }}
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ category.description }}
        </div>
      </v-sheet>
    </div>
  </div>
</template>
<script>
import { mapGetters } from 'vuex'

export default {
  name: 'Category',

  data: () => ({
    category: null,
    tricks: [],
    filter: ''
  }),

  computed: {
    ...mapGetters('tricks', ['categoryById']),
    filteredTricks () {
      if (!this.filter) {
        return this.tricks
      }
      const normalize = this.filter.trim().toLowerCase()
      return this.tricks.filter((t) => {
        return t.name.toLowerCase().includes(normalize) || t.description.toLowerCase().includes(normalize)
      })
    }

  },
  async fetch () {
    const categoryId = this.$route.params.category
    this.category = await this.categoryById(categoryId)
    this.tricks = await this.$axios.$get('/api/categories/' + categoryId + '/tricks')
    console.log(this.tricks)
  },
  head () {
    if (!this.category) {
      return {}
    }
    return {
      title: this.category.name,
      meta: [
        { hid: 'description', name: 'description', content: this.category.description }
      ]
    }
  }
}
</script>

<style scoped>

</style>
