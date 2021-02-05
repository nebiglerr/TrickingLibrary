<template>
  <v-dialog :value="active" persistent>
    <template v-slot:activator="{ on}">
      <v-menu offset-y v-on="on">
        <template v-slot:activator="{ on, attrs }">
          <v-btn depressed v-bind="attrs" v-on="on">
            Create
          </v-btn>
        </template>
        <v-list>
          <v-list-item v-for="(item, i) in menuItems" :key="'ccd-menu-'+i" @click="activate({component:item.component})">
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </template>
    <div v-if="component">
      <component :is="component" />
    </div>
    <div class="d-flex justify-center my-4">
      <v-btn @click="cancelUpload">
        Closed
      </v-btn>
    </div>
  </v-dialog>
</template>

<script>
import { mapMutations, mapState, mapActions } from 'vuex'
import TrickStep from './trick-step'
import SubmissionsSteps from './submission-step'
import CategoryForm from './category-form'
import DifficultyForm from './difficulty-form'

export default {
  name: 'ContentCreationDialog',

  component: {
    TrickStep,
    SubmissionsSteps,
    DifficultyForm,
    CategoryForm

  },
  computed: {
    ...mapState('video-upload', ['active', 'component']),
    menuItems () {
      return [
        { component: TrickStep, title: ' Trick' },
        { component: SubmissionsSteps, title: 'Submission ' },
        { component: DifficultyForm, title: 'Difficulty ' },
        { component: CategoryForm, title: 'Category ' }

      ]
    }
  },
  methods: {
    ...mapMutations('video-upload', ['activate']),
    ...mapActions('video-upload', ['cancelUpload'])
  }
}
</script>

<style scoped>

</style>
