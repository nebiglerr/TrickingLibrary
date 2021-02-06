<template>
  <div>
    <div v-for="s in sections" :key="s.id">
      <div class="d-flex flex-column align-center">
        <p class="text-h5">
          {{ s.title }}
        </p>
        <div>
          <v-btn v-for="item in s.collections" :key="item.id" class="mx-1" :to="s.routeFactory(item.id)">
            {{ item.name }}
          </v-btn>
        </div>
      </div>
      <v-divider class="my-5" />
    </div>
    <div>{{ $config.api }}</div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {

  computed: {
    ...mapState('tricks', ['tricks', 'categories', 'difficulties']),

    sections () {
      return [
        { collections: this.tricks, title: 'Tricks', routeFactory: id => '/trick/' + id },
        { collections: this.categories, title: 'Categories', routeFactory: id => '/category/' + id },
        { collections: this.difficulties, title: 'Difficulties', routeFactory: id => '/difficulty/' + id }
      ]
    }
  }

}
</script>
