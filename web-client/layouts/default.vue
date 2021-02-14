<template>
  <v-app dark>
    <v-app-bar app dark>
      <nuxt-link class="text-h5  text--primary" style="text-decoration: none" to="/">
        Tircking Lirary
      </nuxt-link>

      <v-spacer />
      <v-btn v-if="moderator" class="mx-1" depressed to="/moderation">
        Moderation
      </v-btn>
      <v-skeleton-loader class="mx-1" :loading="loading" transition="fade-transition" type="button">
        <content-creation-dialog />
      </v-skeleton-loader>

      <v-skeleton-loader class="mx-1" :loading="loading" transition="fade-transition" type="button">
        <v-btn v-if="authenticated" depressed outlined>
          <v-icon left>
            mdi-account-circle
          </v-icon>
          Profile
        </v-btn>
        <v-btn v-else depressed outlined @click="$auth.signinRedirect()">
          <v-icon left>
            mdi-account-circle-outline
          </v-icon>
          Sign In
        </v-btn>
      </v-skeleton-loader>
      <v-btn v-if="authenticated" depressed @click="$auth.signoutRedirect()">
        Logout
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import ContentCreationDialog from '../components/content-creation/content-creation-dialog'
export default {
  component: {
    ContentCreationDialog
  },
  computed: {
    ...mapState('auth', ['loading']),
    ...mapGetters('auth', ['authenticated', 'moderator'])
  }

}
</script>
