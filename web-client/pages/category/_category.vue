<template>
  <item-content-layout>
    <template v-slot:content>
      <trick-list :tricks="tricks" class="mx-2" />
    </template>
    <template v-slot:item>
      <div v-if="category">
        <div class="text-h6">
          {{ category.name }}
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ category.description }}
        </div>
      </div>
    </template>
  </item-content-layout>
</template>
<script>
import { mapGetters } from 'vuex'
import TrickList from '~/components/trick-list'
import ItemContentLayout from '~/components/item-content-layout'
export default {
  name: 'Category',
  component: {
    TrickList,
    ItemContentLayout

  },
  data: () => ({
    category: null,
    tricks: [],
    filter: ''
  }),

  computed: {
    ...mapGetters('tricks', ['categoryById'])

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
