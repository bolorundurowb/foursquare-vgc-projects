<template>
  <el-container class="RegisterAttendance">
    <el-main class="RegisterAttendance__main-content">
      <el-row type="flex" justify="center" class="RegisterAttendance__row-container">
        <el-col :xs="24" :sm="12" :md="8" :lg="8" :xl="5">
          <el-card shadow="never">
            <img
                src="@/assets/logo.png"
                alt="logo"
                class="RegisterAttendance__logo"
            >
            <h2 class="RegisterAttendance__title">
              FGC VGC Attendance Registry
            </h2>

            <p>Hello, we'd love to know that you worshipped with us</p>

            <el-form>
              <el-form-item label="Seat Number">
                <el-input
                    v-model="seatNumber"
                    prefix-icon="el-icon-location-outline"
                />
              </el-form-item>
            </el-form>

            <div
                class="RegisterAttendance__person-list"
                v-if="cachedUsers.length"
            >
              <p>Choose a user registered on the device</p>

              <div class="RegisterAttendance__person-container">
                <div
                    class="RegisterAttendance__person-item RegisterAttendance__person"
                    v-for="(person, personIndex) in cachedUsers"
                    :key="person.id"
                    @click="handlePersonClick(personIndex)"
                >
                  <el-avatar icon="el-icon-user-solid"/>
                  <div class="RegisterAttendance__person-details">
                    <p class="RegisterAttendance__person-name">{{ person.firstName }} {{ person.lastName }}</p>
                    <p class="RegisterAttendance__person-phone">{{ person.phoneNumber }}</p>
                    <p class="RegisterAttendance__person-email">{{ person.emailAddress }}</p>
                  </div>
                </div>
              </div>

              <div class="RegisterAttendance__person-item">
                <el-button
                    type="text"
                    v-if="!showNewAttendeeForm"
                    @click="showNewAttendeeForm = true">
                  Can't see your details? Enter them!
                </el-button>
              </div>
            </div>


            <div v-if="showNewAttendeeForm">
              <new-attendee-form
                  @submit="handleNewPersonSubmit"
              />
            </div>
          </el-card>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
import { AlertMixin } from '@/mixins';
import NewAttendeeForm from '@/components/NewAttendeeForm.vue';

export default {
  name: 'RegisterAttendance',
  mixins: [AlertMixin],
  components: { NewAttendeeForm },
  data() {
    return {
      cachedUsers: [],
      selectedPerson: null,
      showNewAttendeeForm: true,
      isRegistering: false,
      seatNumber: ''
    };
  },
  mounted() {
    this.seatNumber = this.$route.query.seatNumber;
    const cachedUsers = localStorage.getItem('cached-attendees');

    if (cachedUsers) {
      try {
        this.cachedUsers = JSON.parse(cachedUsers);
        // show the new attendee by default if there are no cached users
        this.showNewAttendeeForm = !this.cachedUsers.length;
      } catch (error) {
        console.log(error);
      }
    }
  },
  beforeDestroy() {
    this.selectedPerson = null;
  },
  methods: {
    cachePerson(person) {
      person.firstName = person.firstName?.trim();
      person.lastName = person.lastName?.trim();

      const exists = this.cachedUsers.find(user => user.firstName === person.firstName && user.lastName === person.lastName);

      if (!exists) {
        this.cachedUsers.push(person);
      }

      localStorage.setItem('cached-attendees', JSON.stringify(this.cachedUsers));
    }
  }
};
</script>
