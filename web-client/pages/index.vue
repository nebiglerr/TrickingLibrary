<template>
  <div>
    <div>
      <v-btn @click="login">
        Login
      </v-btn>
      <v-btn @click="logout">
        Logout
      </v-btn>
      <v-btn @click="api('test')">
        Api
      </v-btn>
      <v-btn @click="api('mod')">
        Mod
      </v-btn>
    </div>
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
  </div>
</template>

<script>

import { mapState } from 'vuex'
import { UserManager, WebStorageStateStore } from 'oidc-client'

export default {
  data: () => ({
    usrMgr: null
  }),
  computed: {
    ...mapState('tricks', ['tricks', 'categories', 'difficulties']),

    sections () {
      return [
        { collections: this.tricks, title: 'Tricks', routeFactory: id => '/trick/' + id },
        { collections: this.categories, title: 'Categories', routeFactory: id => '/category/' + id },
        { collections: this.difficulties, title: 'Difficulties', routeFactory: id => '/difficulty/' + id }
      ]
    }
  },
  created () {
    if (!process.server) {
      this.usrMgr = new UserManager({
        authority: 'https://localhost:5001',
        client_id: 'web-client',
        redirect_uri: 'https://localhost:3000/oidc/sign-in-callback.html',
        response_type: 'code',
        scope: 'openid profile IdentityServerApi role',
        post_logout_redirect_uri: 'https://localhost:3000',
        // silent_redirect_uri: 'https://localhost:3000',
        userStore: new WebStorageStateStore({ store: localStorage })
        // store: window.localStorage
      })
      this.usrMgr.getUser().then((user) => {
        if (user) {
          this.$axios.setToken('Bearer' + user.access_token)
        }
      })
    }
  },
  methods: {
    login () {
      return this.usrMgr.signinRedirect()
    },
    logout () {
      return this.usrMgr.signoutRedirect()
    },
    api (x) {
      return this.$axios.$get('/api/tricks/' + x).then(msg => console.log(msg))
    }
  }

}
</script>
