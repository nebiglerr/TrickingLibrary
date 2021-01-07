<template>
  <div class="d-flex justify-center align-start">
    <div v-if="submissions" class="mx-2">
      <div v-for="x in 1" :key="x.id">
        <div v-for="s in submissions" :key="s.id">
          {{ s.id }}-{{ s.description }}-{{ s.trickId }}
          <div>
            <video width="400" controls :src="'http://localhost:5000/api/videos/'+s.video" />
          </div>
        </div>
      </div>
    </div>
    <div class="mx-2 sticky">
      <v-sheet class="pa-3 mt-2">
        <div class="text-h6">
          {{ trick.name }}
        </div>
        <v-divider class="my-1" />
        <div class="text-body-2">
          {{ trick.description }}
        </div>
        <div class="text-body-2">
          {{ trick.difficulty }}
        </div>
        <v-divider class="my-1" />
        <div v-for="rd in relatedData" :key="rd.title">
          <div class="text-subtitle-1">
            {{ rd.title }}
          </div>
          <v-chip-group>
            <v-chip v-for="d in rd.data" :key="rd.idFactory(d)" small :to="rd.routeFactory(d)">
              {{ d.name }}
            </v-chip>
          </v-chip-group>
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'

export default {
  async fetch () {
    const trickId = this.$route.params.trick

    await this.$store.dispatch('submissions/fetchSubmissionsForTricks', { trickId }, { root: true })
  },
  computed: {
    ...mapState('submissions', ['submissions']),
    ...mapState('tricks', ['categories', 'tricks']),
    ...mapGetters('tricks', ['trickById']),
    trick () {
      return this.trickById(this.$route.params.trick)
    },
    relatedData () {
      return [
        {
          title: 'Categories',
          data: this.categories.filter(x => this.categories),
          idFactory: c => `category-${c.id}`,
          routeFactory: c => '/'
        },
        {
          title: 'Prerequisites',
          data: this.tricks.filter(x => this.trick.prerequisites),
          idFactory: t => `trick-${t.id}`,
          routeFactory: t => `/tricks/${t.id}`
        },
        {
          title: 'Progressions',
          data: this.tricks.filter(x => this.trick.progressions),
          idFactory: t => `trick-${t.id}`,
          routeFactory: t => `/tricks/${t.id}`
        }
      ]
    }

  },
  head () {
    return {
      title: this.trick.name,
      meta: [
        { hid: 'description', name: 'description', content: this.trick.description }
      ]
    }
  }
}
</script>

<style scoped>
.sticky {
  position: -webkit-sticky;
  position: sticky;
  top: 62px;
}
</style>
